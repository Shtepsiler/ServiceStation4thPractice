from keras._tf_keras.keras.models import Sequential
from keras._tf_keras.keras.layers import Embedding, Bidirectional, LSTM, BatchNormalization, Dense, Dropout
from keras._tf_keras.keras.callbacks import ModelCheckpoint, EarlyStopping
from keras._tf_keras.keras.preprocessing.sequence import pad_sequences
from  keras._tf_keras.keras.models import load_model
import numpy as np
from threading import Thread
from config import config
def build_lstm_model(vocab_size, embedding_dim, max_length, lstm_units, dense_units1, dense_units2, num_classes):
    model = Sequential()
    model.add(Embedding(vocab_size, embedding_dim, input_length=max_length))
    model.add(Bidirectional(LSTM(lstm_units, return_sequences=True, dropout=0.3, recurrent_dropout=0.3)))
    model.add(BatchNormalization())
    model.add(Bidirectional(LSTM(lstm_units // 2, return_sequences=True, dropout=0.3, recurrent_dropout=0.3)))
    model.add(BatchNormalization())
    model.add(Bidirectional(LSTM(lstm_units // 2, dropout=0.3, recurrent_dropout=0.3)))
    model.add(Dense(dense_units1, activation='relu'))
    model.add(Dropout(0.4))
    model.add(BatchNormalization())
    model.add(Dense(dense_units2, activation='relu'))
    model.add(Dropout(0.3))
    model.add(Dense(num_classes, activation='softmax'))
    model.compile(loss='sparse_categorical_crossentropy', optimizer='adam', metrics=['accuracy'])
    print("compiled model")
    return model



def GetModel(vocab_size, num_classes, max_length, logger = None, embedding_dim = 128, lstm_units=256, dense_units1 = 128, dense_units2 = 64):
    try:
        model = load_model(config.lstm_model_path)
        if logger != None : logger.info("Model loaded successfully.")
    except Exception as e:
        if logger != None : logger.exception("Error loading model, creating new: %s", e)
        model = build_lstm_model(vocab_size, embedding_dim, max_length, lstm_units, dense_units1, dense_units2, num_classes)
    print("Get model")

    return  model


def train_model(model, X_train, y_train, X_test, y_test, model_checkpoint_path = config.checkpoint_path):
    checkpoint_callback = ModelCheckpoint(
        model_checkpoint_path, monitor='val_accuracy', save_best_only=True, mode='max', verbose=1)
    early_stopping_callback = EarlyStopping(
        monitor='val_accuracy', patience=5, mode='max', verbose=1, restore_best_weights=True)
    print("start training")
    model.fit(
        X_train, y_train, epochs=5, batch_size=100,
        validation_data=(X_test, y_test),
        callbacks=[checkpoint_callback, early_stopping_callback]
    )
    print("end training")



def evaluate_model(model, X_test, y_test):
    print("start evaluation")
    loss, accuracy = model.evaluate(X_test, y_test)
    print("end evaluation")
    print(f'Accuracy: {accuracy:.2f}')
    print(f'Loss: {loss:.2f}')
    return loss, accuracy


def predict_mechanic(problem_description, model, tokenizer, max_length, label_encoder, preprocess_text, custom_stop_words):
    if tokenizer is None:
        raise ValueError("Tokenizer is not initialized. Ensure it's loaded properly before inference.")
    text_processed = preprocess_text(problem_description, custom_stop_words)
    sequence = tokenizer.texts_to_sequences([text_processed])
    padded_sequence = pad_sequences(sequence, maxlen=max_length)                #d
    predicted_probs = model.predict(padded_sequence)
    predicted_class_index = np.argmax(predicted_probs)
    predicted_class = label_encoder.inverse_transform([predicted_class_index])[0]
    confidence = predicted_probs[0][predicted_class_index]
    print("predict_mechanic")
    return predicted_class, confidence


def train_model_in_thread(model, X_train, y_train, X_test, y_test, model_checkpoint_path=config.checkpoint_path):
    train_thread = Thread(target=train_model, args=(model, X_train, y_train, X_test, y_test, model_checkpoint_path))
    train_thread.start()
    return train_thread  # Return the thread if you want to monitor or join it later
