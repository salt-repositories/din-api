#!groovy​

@Library('main_shared') _

String repo = 'din-api'
String project = 'Din.Application.WebAPI'
String solution = 'Din'

Map settings = [
  sonar_key: 'din-api',
  unit_test_projects: ["Din.Domain.Tests"],
  migrations_project: 'Din.Infrastructure.Migrations'
]

csDockerBuildAndPublish(repo, project, solution, settings)