node {
    def app
    def branch
    try {
      stage('Clone') {
          checkout scm
          branch = env.BRANCH_NAME
          echo 'Repo cloned'
      }

      stage('Build & Test') {
          app = docker.build("saltz/din-api")
          echo 'Build successful'
      }

      stage('Deploy') {
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
            case 'dev':
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
