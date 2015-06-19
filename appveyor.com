version: 1.0.0.{build}
assembly_info:
  patch: true
  file: '**\AssemblyInfo.*'
  assembly_version: 1.0.0
  assembly_file_version: '{version}'
  assembly_informational_version: '{version}'
build:
  publish_nuget: true
  publish_nuget_symbols: true
  include_nuget_references: true
  parallel: true
  verbosity: minimal
