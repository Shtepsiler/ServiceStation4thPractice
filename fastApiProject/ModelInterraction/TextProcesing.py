import re
import joblib
import nltk
from nltk.stem import WordNetLemmatizer
from keras._tf_keras.keras.preprocessing.text import Tokenizer
from sklearn.preprocessing import LabelEncoder
from config import config
import sklearn
from packaging import version


def ensure_nltk_data():
    required_packages = ['stopwords', 'wordnet', 'omw-1.4']
    for package in required_packages:
        try:
            nltk.data.find(f'corpora/{package}')
        except LookupError:
            nltk.download(package, quiet=True)

ensure_nltk_data()
lemmatizer = WordNetLemmatizer()

def preprocess_text(text, custom_stop_words):
    text = text.lower()
    text = re.sub(r'[^a-zа-яієїґ]', ' ', text)
    words = text.split()
    words = [lemmatizer.lemmatize(word) for word in words if word not in custom_stop_words]
    return ' '.join(words)


def GetTokenizer(logger=None):
    tokenizer = None
    try:
        tokenizer = joblib.load(config.tokenizer_path)
        if logger: logger.info("tokenizer loaded successfully.")
    except Exception as e:
        if logger: logger.warning(f"Error loading tokenizer, creating new: {e}")
        tokenizer = Tokenizer(num_words=config.max_words, oov_token="<OOV>")
    return tokenizer


def GetLabel_Encoder(logger=None):
    label_encoder = None
    try:
        # Перевір версію перед завантаженням
        current_version = version.parse(sklearn.__version__)
        if current_version < version.parse("1.6.0"):
            if logger: logger.warning("Sklearn version mismatch detected, recreating label encoder")
            raise Exception("Version mismatch")

        label_encoder = joblib.load(config.label_encoder_path)
        if logger: logger.info("label_encoder loaded successfully.")
    except Exception as e:
        if logger: logger.warning(f"Error loading label_encoder, creating new: {e}")
        label_encoder = LabelEncoder()
    return label_encoder