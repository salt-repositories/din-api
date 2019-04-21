node('dotnetcore') {
  def branch
  stage('Clone') {
    checkout scm
    branch = env.BRANCH_NAME
  }

  stage('Restore packages') {
    sh "dotnet restore src/"
  }

  stage('Clean') {
    sh "dotnet clean src/"
  }

  stage('Build') {
    sh "dotnet build src/ --configuration Release"
  }

  stage('Test') {
    sh 'dotnet test src/Din.UnitTests/ --logger "trx;LogFileName=results.xml"'
    xunit thresholds: [failed(failureNewThreshold: '3', failureThreshold: '5', unstableNewThreshold: '1', unstableThreshold: '2')], tools: [MSTest(deleteOutputFiles: true, failIfNotNew: true, pattern: '**/results.xml', skipNoTestFiles: false, stopProcessingIfError: true)]
  }

  stage('Deploy to environment') {
    parallel devolopment: {
        node('docker') {
          def app
          if (branch == 'develop') {
            stage('Clone') {
              checkout scm
            }

            stage('Build docker image') {
              app = docker.build('saltz/din-api')
            }

            stage('Push docker image') {
              docker.withRegistry('https://registry.hub.docker.com', 'docker-hub-credentials') {
                app.push('dev')
              }
            }

            stage('Cleanup') {
              sh 'docker image prune -f -a'
            }
          }
        }
      },
      production: {
        node('docker') {
          def app
          if (branch == 'master') {
            stage('Clone') {
              checkout scm
            }

            stage('Build docker image') {
              app = docker.build('saltz/din-api')
            }

            stage('Push docker image') {
              docker.withRegistry('https://registry.hub.docker.com', 'docker-hub-credentials') {
                app.push('prod')
              }
            }

            stage('Cleanup') {
              sh 'docker image prune -f -a'
            }
          }
        }
      }
  }

  stage('Cleanup') {
    step([$class: 'WsCleanup'])
  }
}