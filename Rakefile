require 'bundler/setup'
require 'fileutils'

require 'albacore'
require 'albacore/tasks/versionizer'

Configuration = ENV['CONFIGURATION'] || 'Release'

Albacore::Tasks::Versionizer.new :versioning

desc 'create assembly infos'
asmver_files :assembly_info do |a|
  a.files = FileList['**/*proj'] # optional, will find all projects recursively by default

  a.attributes assembly_description: 'TODO',
               assembly_configuration: Configuration,
               assembly_company: 'CHAOS APS',
               assembly_copyright: "(c) 2015 by CHAOS APS",
               assembly_version: ENV['LONG_VERSION'],
               assembly_file_version: ENV['LONG_VERSION'],
               assembly_informational_version: ENV['BUILD_VERSION']
end

desc 'Perform fast build (warn: doesn\'t d/l deps)'
build :quick_compile do |b|
  b.prop 'Configuration', Configuration
  b.logging = 'quiet'
  b.sln     = 'Portal.sln'
end

desc "Run all the tests"
test_runner :tests => [:package_tests]do |tests|
    tests.files = FileList['tests/Chaos.Test.dll']
    tests.add_parameter '/framework=4.0.30319'
    tests.exe = 'tools/NUnit-2.6.0.12051/bin/nunit-console.exe'
  end

task :package_tests => [:quick_compile] do |cmd|
	FileUtils.mkdir 'tests'

  FileUtils.cp 'tools/NUnit.2.6.0.12051/bin/nunit.framework.dll', 'tests/'
  FileUtils.cp 'src/app/Chaos.Portal.Core/bin/Release/Chaos.Portal.Core.dll', 'tests/'
  FileUtils.cp 'src/app/Chaos.Portal.v5/bin/Release/Chaos.Portal.v5.dll', 'tests/'
  FileUtils.cp 'src/app/Chaos.Portal.v6/bin/Release/Chaos.Portal.v6.dll', 'tests/'
  FileUtils.cp 'src/app/Chaos.Portal/bin/Release/Chaos.Portal.dll', 'tests/'

  system 'tools/ILMerge/ILMerge.exe',
    ['/out:tests\Chaos.Portal.Test.dll',
      '/target:library',
      '/ndebug',
      '/lib:lib',
      '/targetplatform:v4,c:\windows\Microsoft.Net\Framework64\v4.0.30319',
      '/lib:c:\windows\Microsoft.Net\Framework64\v4.0.30319',
      'src\test\CHAOS.Portal.Test\bin\Release\CHAOS.Portal.Test.dll',
      'src\test\Chaos.Portal.IntegrationTest\bin\Release\Chaos.Portal.IntegrationTest.dll',
      'src\test\Chaos.Portal.Protocol.Tests\bin\Release\Chaos.Portal.Protocol.Tests.dll'], clr_command: true
end

desc "Merges all production assemblies"
task :package => [:tests] do |cmd|
	FileUtils.mkdir 'build'

  system 'tools/ILMerge/ILMerge.exe',
    ['/out:build\Chaos.Portal.dll',
      '/target:library',
      '/ndebug',
      '/lib:lib',
      '/lib:c:\windows\Microsoft.Net\Framework64\v4.0.30319',
      'src/app/Chaos.Portal.Core/bin/Release/Chaos.Portal.Core.dll',
      'src/app/Chaos.Portal.v5/bin/Release/Chaos.Portal.v5.dll',
      'src/app/Chaos.Portal.v6/bin/Release/Chaos.Portal.v6.dll',
      'src/app/Chaos.Portal/bin/Release/Chaos.Portal.dll'], clr_command: true
end

task :default => :package
