# -*- coding: utf-8 -*-
"""Final DM.ipynb

Automatically generated by Colaboratory.

Original file is located at
    https://colab.research.google.com/drive/1Q9PwDg_bY4NM1Yu0pwKPBy9kM_txvZZC
"""

import numpy as np
import pandas as pd
import matplotlib.pyplot as plt
import seaborn as sns

from sklearn.impute import SimpleImputer
from sklearn.model_selection import train_test_split
from sklearn.preprocessing import MinMaxScaler
from sklearn.pipeline import make_pipeline
from sklearn.dummy import DummyClassifier

from sklearn.linear_model import LogisticRegression, LinearRegression

from sklearn.metrics import accuracy_score , ConfusionMatrixDisplay , classification_report,r2_score

data= pd.read_csv("/content/framingham.csv")

data.shape

data.info()

data.describe()

#EDA
#Missing data
data.isnull().sum()

#Handle missing value
data["education"].fillna(data["education"].mode()[0],inplace=True)
data["cigsPerDay"].fillna(data["cigsPerDay"].mode()[0],inplace=True)
data["BPMeds"].fillna(data["BPMeds"].mode()[0],inplace=True)
data["totChol"].fillna(data["totChol"].mode()[0],inplace=True)
data["BMI"].fillna(data["BMI"].mode()[0],inplace=True)
data["heartRate"].fillna(data["heartRate"].mode()[0],inplace=True)
data["glucose"].fillna(data["glucose"].mode()[0],inplace=True)

corr=data.drop(columns='TenYearCHD').corr()
plt.subplots(figsize=(25,20))
sns.heatmap(corr,annot=True)

def draw_histograms(dataframe, features, rows, cols):
    fig=plt.figure(figsize=(20,20))
    for i, feature in enumerate(features):
        ax=fig.add_subplot(rows,cols,i+1)
        dataframe[feature].hist(bins=20,ax=ax,facecolor='midnightblue')
        ax.set_title(feature+" Distribution",color='DarkRed')

    fig.tight_layout()
    plt.show()
draw_histograms(data,data.columns,6,3)

#Preprocessing
#Define X and Y
X= data.drop('TenYearCHD', axis='columns')
Y=data['TenYearCHD']

X_train, X_test, y_train, y_test = train_test_split(X,Y,test_size=0.2 , shuffle=True , random_state=42)

scaler = MinMaxScaler()
X = scaler.fit_transform(X)

dummy_classifier = DummyClassifier(strategy='most_frequent')
dummy_classifier.fit(X_train , y_train)
y_pred = dummy_classifier.predict(X_test)
accuracy = accuracy_score(y_test , y_pred)
print(f"Baseline Model Accuracy: {accuracy:.4f}")

Logis=make_pipeline(
    SimpleImputer(strategy='mean'),
    MinMaxScaler(),
    LogisticRegression()
)
Logis.fit(X_train , y_train)

Logis.score(X_train,y_train)

Logis_pred=Logis.predict(X_test)

Logis_score=accuracy_score(y_test,Logis_pred)
Logis_score

from sklearn.metrics import mean_squared_error, r2_score
mse = mean_squared_error(y_test, Logis_pred)
r2 = r2_score(y_test, Logis_pred)
print("Mean squared error:", mse)
print("R-squared:", r2)
#Giá trị R^2 trong logistic regression thường thấp hơn linear regression và có thể mang cả giá trị âm.

# Để đánh giá mô hình Logistic => Sử dụng các chỉ số phù hợp với mục đích phân loại:
#Accuracy: Tỉ lệ dự đoán chính xác.
#Sensitivity: Tỉ lệ phân loại chính xác các trường hợp thực sự thuộc lớp target.
#Specificity: Tỉ lệ phân loại chính xác các trường hợp thực sự không thuộc lớp target.
#F1-score: Chỉ số cân bằng giữa precision (chính xác) và recall (độ bao phủ).
from sklearn.metrics import accuracy_score, precision_score, recall_score, f1_score, confusion_matrix
cm = confusion_matrix(y_test, Logis_pred)

accuracy = accuracy_score(y_test, Logis_pred)
precision = precision_score(y_test, Logis_pred)
recall = recall_score(y_test, Logis_pred)
f1 = f1_score(y_test, Logis_pred)
# Extract elements from the confusion matrix
tn, fp, fn, tp = cm.ravel()

# Calculate additional metrics
npv = tn / (tn + fn)  # Negative Predictive Value
specificity = tn / (tn + fp)  # Specificity


commnet_acc = f"Mô hình đạt độ chính xác {accuracy:.2%}. Điều này có nghĩa là tỷ lệ dự đoán đúng về rủi ro mắc bệnh "
comment_pre = f"Tỷ lệ precision là {precision:.2%}. Có nghĩa là  {precision:.2%} trong số các trường hợp được dự đoán là gặp rủi ro mắc bệnh thì thực sự là mắc bệnh. "
comment_recall = f"Tỷ lệ recall là {recall:.2%}. Tức là mô hình đoán đúng {recall:.2%} các trường hợp gặp rủi ro thực sự "
comment_f1 = f"F1-score đạt {f1:.2f}. Là một độ đo kết hợp giữa precision và recall, là trung bình cộng của precision và recall, được điều chỉnh theo độ nặng của precision và recall."
comment_npv = f"Tỷ lệ NPV là {npv:.2%}. Điều này cho biết {npv:.2%} trong số các trường hợp được dự đoán là không gặp mắc bệnh thì thực sự không mắc bệnh"
# Bình luận về Specificity
comment_spe = f"Tỷ lệ specificity là {specificity:.2%}. Điều này là độ đo về khả năng mô hình loại bỏ đúng các trường hợp không mắc bệnh"

print(commnet_acc+"\n" +comment_pre+"\n" +comment_recall+"\n" +comment_f1+"\n" +comment_npv+"\n"+comment_spe+"\n"  )

ConfusionMatrixDisplay.from_estimator(Logis , X_test , y_test);

print(classification_report(y_test,Logis_pred))

# 0,61,3,1,30,0,0,1,0,225,150,95,28.58,65,103,1
# Example new data
new_data = pd.DataFrame({
    'male': [1],
    'age': [61],
    'education': [3],
    'currentSmoker': [1],
    'cigsPerDay': [35],
    'BPMeds': [0],
    'prevalentStroke': [0],
    'prevalentHyp': [1],
    'diabetes': [0],
    'totChol': [225],
    'sysBP': [150],
    'diaBP': [95],
    'BMI': [28.58],
    'heartRate': [65],
    'glucose': [103],
})

new_predictions = Logis.predict_proba(new_data)
print(f'Predicted TenYearCHD: {new_predictions[0]}')
predictions=Logis.predict(new_data)
print(predictions[0])

Linear = LinearRegression()
Linear.fit(X_train,y_train)

Linear.score(X_train,y_train)

Linear_pred=Linear.predict(X_test)

from sklearn.metrics import mean_squared_error, r2_score
mse = mean_squared_error(y_test, Linear_pred)
r2 = r2_score(y_test, Linear_pred)
print("Mean squared error:", mse)
print("R-squared:", r2)

new_predictions_1 = Linear.predict(new_data)
print(f'Predicted TenYearCHD: {new_predictions_1[0]}')

from sklearn.linear_model import Lasso

lasso_model = Lasso(alpha=0.0000000000000005)  # You can adjust the alpha value

# Fit the model to the training data
lasso_model.fit(X_train, y_train)

# Make predictions on the testing data
y_pred = lasso_model.predict(X_test)

# Evaluate the model
r2 = r2_score(y_test, y_pred)
print(f"R-squared (R2) score: {r2}")

from sklearn.linear_model import Ridge

ridge_model = Ridge(alpha=0.0000000000000005)  # You can adjust the alpha value

# Fit the model to the training data
ridge_model.fit(X_train, y_train)

# Make predictions on the testing data
y_pred = ridge_model.predict(X_test)

# Evaluate the model
r2 = r2_score(y_test, y_pred)
print(f"R-squared (R2) score: {r2}")

from sklearn.linear_model import SGDRegressor

sgd_regressor = SGDRegressor(max_iter=1000, tol=1e-3, alpha=0.01, learning_rate="invscaling", eta0=0.01)
sgd_regressor.fit(X_train, y_train)
y_pred = ridge_model.predict(X_test)

# Evaluate the model
r2 = r2_score(y_test, y_pred)
print(f"R-squared (R2) score: {r2}")