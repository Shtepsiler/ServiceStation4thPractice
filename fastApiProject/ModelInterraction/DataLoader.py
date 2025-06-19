import pandas as pd
from keras._tf_keras.keras.preprocessing.text import Tokenizer
from keras._tf_keras.keras.preprocessing.sequence import pad_sequences
import numpy as np
from sklearn.preprocessing import LabelEncoder

from ModelInterraction.TextProcesing import GetLabel_Encoder,preprocess_text,GetTokenizer
from sklearn.model_selection import train_test_split

def load_data(file_path):
    df = pd.read_csv(file_path)
    print("load data")
    return df
def save_data(df, file_path):
    df.to_csv(file_path, index=False)
    print(f"DataFrame has been saved to {file_path}")

def load_stopwords(file_path):
    with open(file_path, 'r', encoding='utf-8') as f:
        stop_words = set(f.read().splitlines())
    print("load_stopwords")
    return stop_words

def load_dataset(df, custom_stop_words, test_size = 0.1,file_path = "Data/mechanic_data.csv"):
    random_state = 42
    # Apply text preprocessing
    custom_stop_words = custom_stop_words
    df['problem_description'] = df['problem_description'].apply(lambda x: preprocess_text(x, custom_stop_words))

    # Separate features and labels
    X = df['problem_description']
    y = df['mechanic_class']


    # Encode labels
    label_encoder = GetLabel_Encoder(None)
    if label_encoder == None: label_encoder = LabelEncoder()
    y_encoded = label_encoder.fit_transform(y)
    num_classes = len(np.unique(y_encoded))  # Number of classes

    # Tokenize text
    tokenizer = GetTokenizer(None)

    if tokenizer == None : tokenizer = Tokenizer(num_words=7000)
    tokenizer.fit_on_texts(X)
    vocab_size = len(tokenizer.word_index) + 1  # Vocabulary size
    X_sequences = tokenizer.texts_to_sequences(X)

    # Determine maximum sequence length
    max_length = max(len(seq) for seq in X_sequences)
    X_padded = pad_sequences(X_sequences, maxlen=max_length)

    # Split into training and testing sets
    X_train, X_test, y_train, y_test = train_test_split(X_padded, y_encoded, test_size=test_size,
                                                        random_state=random_state)
    print("load_dataset")

    return X_train, X_test, y_train, y_test, vocab_size, max_length, num_classes, label_encoder, tokenizer

def get_labels(df):
    """
    This method loads the dataset and returns the unique labels.
    """
    y = df['mechanic_class']
    label_encoder = GetLabel_Encoder(None)
    if label_encoder is None:
        label_encoder = LabelEncoder()
    y_encoded = label_encoder.fit_transform(y)
    labels = label_encoder.classes_
    print("get_labels")
    return labels