# MSG Foundation 

## Introduction

This is the repository of the case laboratory "MSG Foundation" used with academic purposes. 

### What does this repository contain? 

This repository contains the source, in .NET of several functions required to be used for the execution of the MSG Foundation laboratory case. This lab case is used for working with Camunda Platform 7 (a BPM Engine) in business processes execution.  

The code published here is for being used locally in a pc. 

## ¿How to use this code? 

Using this repository involves: 

1. Installing a [PostgreSQL](https://www.postgresql.org/) instance, with PgAdmin. 
2. Cloning this repository. 
3. Running the Camunda Engine. 
4. Deploying this repo on .NET (it requires previous item)
5. (Only once) Creating the MSG Foundation database. 

## Detailed step-by-step. 

### 1. Installing a [PostgreSQL](https://www.postgresql.org/) instance, with PgAdmin. 

Go to the [PostgeSQL download page](https://www.enterprisedb.com/downloads/postgres-postgresql-downloads) and use your appropiate operating system package. 

Install all the features. Important to include the Builder. 

Please use "root" as password. 

### 2. Cloning this repository.

Go to the [GitHub](https://github.com/RogerRoldan/MsgFoundation-Camunda-with-.Net-) repository and clone this repo. Remember to go to the Code button for obtaining the link for Git Clone.  

### 3. Running the Camunda Engine. 

Go to your local folder of the Camunda Engine and ejecute the start.bat file. 

### 4. Deploying this repo on .NET (it requires previous item)

Open the folder of the repo with Visual Studio Code, open a Terminal (Terminal -> New Terminal) and execute dotNet run 

### 5. (Only once) Creating de MSG Foundation database. 

Copy the link where the .NET proyect was deployed (in my case http://localhost:5152) and on the internet browser add the string "/dbconexion" and press ENTER. 
