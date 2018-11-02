pipeline {
  agent {
    dockerfile {
      filename 'Dockerfile'
    }

  }
  stages {
    stage('Build and test project') {
      steps {
        sh 'docker.build(\'saltz/din-api\')'
      }
    }
  }
}
