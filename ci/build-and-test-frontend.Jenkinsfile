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
                sh "chmod +x ./frontend/scripts/run-unit-tests.sh"
                sh "./frontend/scripts/run-unit-tests.sh"
            }
            
        }
        
        stage('Build and Push Frontend image') {
            steps {
                sh "chmod +x ./frontend/scripts/build-docker-image.sh"
                sh "./frontend/scripts/build-docker-image.sh"
            }
        }
    }
}
