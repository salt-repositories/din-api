pipeline {
  agent {
    docker {
      image 'microsoft/dotnet:2.1-sdk'
      args '-u root:root'
    }

  }
  stages {
    stage('Restore packages') {
      steps {
        sh 'dotnet restore src/'
      }
    }
    stage('Build') {
      steps {
        sh 'dotnet build src/'
      }
    }
    stage('Test') {
      steps {
        sh 'dotnet test src/Din.Tests/ --logger "trx;LogFileName=results.xml"'
        script {
          xunit thresholds: [failed(failureNewThreshold: '3', failureThreshold: '5', unstableNewThreshold: '1', unstableThreshold: '2')], tools: [MSTest(deleteOutputFiles: true, failIfNotNew: true, pattern: '**/results.xml', skipNoTestFiles: false, stopProcessingIfError: true)]
        }

      }
    }
    stage('Deploy to environment') {
      parallel {
        stage('Deploy to development') {
          agent any
          when {
            branch 'develop'
          }
          steps {
            script {
              def app = docker.build('saltz/din-api')
              docker.withRegistry('https://registry.hub.docker.com', 'docker-hub-credentials') {
                app.push('dev')
              }
            }

          }
        }
        stage('Deploy to production') {
          agent any
          when {
            branch 'master'
          }
          steps {
            script {
              def app = docker.build('saltz/din-api')
              docker.withRegistry('https://registry.hub.docker.com', 'docker-hub-credentials') {
                app.push('prod')
              }
            }

          }
        }
      }
    }
  }
}