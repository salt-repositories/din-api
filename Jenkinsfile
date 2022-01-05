#!groovy​

@Library('main_shared') _

String organization = 'saltrepositories'
String repo = 'din-api'
String project = 'Din.Application.WebAPI'
String solution = 'Din'

Map settings = [
  sonar_key: 'din-api',
  unit_test_projects: ["Din.Domain.Tests"],
]

csDockerBuildAndPublish(organization, repo, project, solution, settings)