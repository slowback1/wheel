pipeline {
    agent any

    stages {
        stage('Pull from git') {
            steps {
                git branch: 'main', credentialsId: 'f9aa0f7e-965b-4827-b61b-85f309845cee', url: 'git@github.com:slowback1/wheel.git'
            }
        }
        
        stage('Run Unit Tests') {
            steps {
                sleep(10)
            }
        }
        
        stage('Build and Push Backend image') {
            steps {
                sh "chmod +x ./api/scripts/build-and-push-backend.sh"
                sh "./api/scripts/build-and-push-backend.sh"
            }
        }
    }
}