# -*- encoding: utf-8 -*-

Gem::Specification.new do |s|
  s.name = "albacore"
  s.version = "0.3.5"

  s.required_rubygems_version = Gem::Requirement.new(">= 0") if s.respond_to? :required_rubygems_version=
  s.authors = ["Henrik Feldt", "Anthony Mastrean"]
  s.date = "2013-02-07"
  s.description = "Easily build your .Net or Mono project using this collection of Rake tasks."
  s.email = "henrik@haf.se"
  s.homepage = "http://albacorebuild.net"
  s.require_paths = ["lib"]
  s.rubyforge_project = "albacore"
  s.rubygems_version = "2.0.0"
  s.summary = "Dolphin-safe and awesome Mono and .Net Rake-tasks"

  if s.respond_to? :specification_version then
    s.specification_version = 3

    if Gem::Version.new(Gem::VERSION) >= Gem::Version.new('1.2.0') then
      s.add_runtime_dependency(%q<rubyzip>, [">= 0"])
    else
      s.add_dependency(%q<rubyzip>, [">= 0"])
    end
  else
    s.add_dependency(%q<rubyzip>, [">= 0"])
  end
end
