node {
    def app
    def branch
    try {
      stage('clone') {
          checkout scm
          branch = env.BRANCH_NAME
          echo 'Repo cloned'
      }

      stage('restore packages') {
        sh "dotnet restore src/"
      }

      stage('clean') {
        sh "dotnet clean src/"
      }

      stage('build') {
        sh "dotnet build src/ --configuration Release"
      }

      stage('test') {
        sh 'dotnet test src/Din.Tests/ --logger "trx;LogFileName=results.xml"'
        xunit([MSTest(deleteOutputFiles: true, failIfNotNew: true, pattern: '**/results.xml', skipNoTestFiles: false, stopProcessingIfError: true)])
      }

      stage('create docker image') {
        app = docker.build("saltz/din-api")
        docker.withRegistry('https://registry.hub.docker.com', 'docker-hub-credentials') {
          switch(branch) {
            case 'master':
              app.push('prod');
              echo 'Deployed to production environment'
              break
            case 'beta':
                app.push('beta');
                echo 'Deployed to beta environment'
              break
            case 'develop':
                app.push('dev')
                echo 'Deployed to dev environment'
              break
            default:
                echo 'This build will not be deployed'
              break
          }
        }
      }
    } catch (e) {
        echo 'Pipeline failed'
      throw e
    }
}
