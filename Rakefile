require 'bundler/setup'
require 'fileutils'

require 'albacore'
require 'albacore/tasks/versionizer'

Configuration = ENV['CONFIGURATION'] || 'Release'

Albacore::Tasks::Versionizer.new :versioning

task :clean do
  FileUtils.rmtree 'tests'
  FileUtils.rmtree 'build'
end

task :prepare_compile => [:clean] do
  FileUtils.cp 'src/test/Chaos.Portal.IntegrationTest/App.config.sample', 'src/test/Chaos.Portal.IntegrationTest/App.config'
  FileUtils.cp 'src/test/Chaos.Portal.Protocol.Tests/App.config.sample', 'src/test/Chaos.Portal.Protocol.Tests/App.config'
end

desc 'Perform fast build (warn: doesn\'t d/l deps)'
build :quick_compile => [:prepare_compile] do |b|
  b.prop 'Configuration', Configuration
  b.logging = 'minimal'
  b.sln     = 'Portal.sln'
end

task :package_tests => [:quick_compile] do
  FileUtils.mkdir 'tests'

  FileUtils.cp 'tools\NUnit-2.6.0.12051\bin\nunit.framework.dll', 'tests'
  FileUtils.cp 'src\app\Chaos.Portal.Core\bin\Release\Chaos.Portal.Core.dll', 'tests'
  FileUtils.cp 'src\app\Chaos.Portal.v5\bin\Release\Chaos.Portal.v5.dll', 'tests'
  FileUtils.cp 'src\app\Chaos.Portal.v6\bin\Release\Chaos.Portal.v6.dll', 'tests'
  FileUtils.cp 'src\app\Chaos.Portal\bin\Release\Chaos.Portal.dll', 'tests'
  FileUtils.cp 'src\test\Chaos.Portal.Protocol.Tests\App.config', 'tests\Chaos.Portal.Test.dll.config'
  FileUtils.cp 'tools\Moq.4.0.10827\NET40\Moq.dll', 'tests'
  FileUtils.cp 'lib\CHAOS.dll', 'tests'
  FileUtils.cp 'packages\AWSSDK.2.3.24.3\lib\net45\AWSSDK.dll', 'tests'
  FileUtils.cp 'packages\CouchbaseNetClient.1.3.9\lib\net40\Couchbase.dll', 'tests'
  FileUtils.cp 'packages\CouchbaseNetClient.1.3.9\lib\net40\Enyim.Caching.dll', 'tests'
  FileUtils.cp 'packages\Newtonsoft.Json.6.0.5\lib\net45\Newtonsoft.Json.dll', 'tests'
  FileUtils.cp_r 'src\test\CHAOS.Portal.Test\bin\Release\EmailService', 'tests'

  system 'tools/ILMerge/ILMerge.exe',
    ['/out:tests\Chaos.Portal.Test.dll',
     '/target:library',
     '/ndebug',
     '/lib:lib',
     '/targetplatform:v4,c:\windows\Microsoft.Net\Framework64\v4.0.30319',
     '/lib:c:\windows\Microsoft.Net\Framework64\v4.0.30319',
     'src\test\CHAOS.Portal.Test\bin\Release\CHAOS.Portal.Test.dll',
     'src\test\Chaos.Portal.Protocol.Tests\bin\Release\Chaos.Portal.Protocol.Tests.dll'], clr_command: true
end

desc "Run all the tests"
test_runner :tests => [:package_tests] do |tests|
  tests.files = FileList['tests/Chaos.Portal.Test.dll']
  tests.add_parameter '/framework=4.0.30319'
  tests.exe = 'tools/NUnit-2.6.0.12051/bin/nunit-console.exe'
end

desc "Merges all production assemblies"
task :package => [:tests] do
  FileUtils.mkdir 'build'

  system 'tools/ILMerge/ILMerge.exe',
    ['/out:build\Chaos.Portal.dll',
      '/target:library',
      '/ndebug',
      '/lib:lib',
      '/targetplatform:v4,c:\windows\Microsoft.Net\Framework64\v4.0.30319',
      '/lib:c:\windows\Microsoft.Net\Framework64\v4.0.30319',
      'src/app/Chaos.Portal.Core/bin/Release/Chaos.Portal.Core.dll',
      'src/app/Chaos.Portal.v5/bin/Release/Chaos.Portal.v5.dll',
      'src/app/Chaos.Portal.v6/bin/Release/Chaos.Portal.v6.dll',
      'src/app/Chaos.Portal/bin/Release/Chaos.Portal.dll'], clr_command: true
end

task :default => :package
