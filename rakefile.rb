require 'rubygems'
require 'albacore'

company_name      = 'CHAOS ApS'
product_name      = 'Portal'
copyright         = company_name + ' (c) 2013'
solution_path     = product_name + '.sln'
unit_tests        = ['src/test/CHAOS.Portal.Test/bin/Debug/Chaos.Portal.Test.dll']
itegraion_tests   = ['src/test/Chaos.Portal.IntegrationTest/bin/Debug/Chaos.Portal.IntegrationTest.dll']
assemblyinfo_path = 'src/app/Chaos.Portal/Properties/AssemblyInfo.cs'
compiled_assemblies = ['src/app/Chaos.Portal/bin/Debug/Chaos.Portal.dll']
packaged_dll      = 'Chaos.Portal.dll'

desc 'performs a clean build'
task :default => [:unittests]

task :clean do
	puts 'clean'
end

assemblyinfo :assemblyinfo => [:clean] do |asm|
	asm.company_name = company_name
	asm.product_name = product_name
	asm.copyright    = copyright
	asm.output_file  = assemblyinfo_path
end

msbuild :compile => [:assemblyinfo] do |msb| 
	msb.properties :configuration => :Debug
	msb.targets    :build
	
	msb.solution = solution_path
end

nunit :unittests => [:compile] do |nunit|
	nunit.command    = 'tools/NUnit-2.6.0.12051/bin/nunit-console.exe'
	nunit.assemblies = unit_tests
end

nunit :integrationtests => [:unittests] do |nunit|
	nunit.command    = 'tools/NUnit-2.6.0.12051/bin/nunit-console.exe'
	nunit.assemblies = itegraion_tests
end

ilmerge :package => [:unittests] do |cfg|
	cfg.command    = 'tools/ILMerge/ILMerge.exe'
	cfg.assemblies = compiled_assemblies
	cfg.output     = 'deploy/' + packaged_dll
end