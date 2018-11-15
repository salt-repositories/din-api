node {
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
    sh 'dotnet test src/Din.Tests/ --logger "trx;LogFileName=results.xml"'
    xunit thresholds: [failed(failureNewThreshold: '3', failureThreshold: '5', unstableNewThreshold: '1', unstableThreshold: '2')], tools: [MSTest(deleteOutputFiles: true, failIfNotNew: true, pattern: '**/results.xml', skipNoTestFiles: false, stopProcessingIfError: true)]
  }

  stage('Deploy to environment') {
    parallel devolopment: {
        node {
          def app
          if (branch == 'develop') {
            checkout scm
            app = docker.build('saltz/din-api')
            docker.withRegistry('https://registry.hub.docker.com', 'docker-hub-credentials') {
              app.push('dev')
            }
          }
        }
      },
      production: {
        node {
          def app
          if (branch == 'master') {
            checkout scm
            app = docker.build('saltz/din-api')
            docker.withRegistry('https://registry.hub.docker.com', 'docker-hub-credentials') {
              app.push('prod')
            }
          }
        }
      }
  }
}