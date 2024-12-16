#!/bin/bash

VER="$1"
if [[ "$1" == "" ]]; then
	echo  "Usage: $0 <Package version must be entered>"
	:
else
	#Remove previously Release folder
	rm -rf Api/MG.BuildingBlock.Api/bin/Release/
	rm -rf Application/MG.BuildingBlock.Application/bin/Release/
	rm -rf Domain/MG.BuildingBlock.Domain/bin/Release/
	rm -rf Resources/MG.BuildingBlock.Resources/bin/Release/
	rm -rf Infra/MG.BuildingBlock.Infra/bin/Release/
	rm -rf Infra/MG.BuildingBlock.Infra.CrossCutting.Bus/bin/Release/
	rm -rf Infra/MG.BuildingBlock.Infra.CrossCutting.Identity/bin/Release/
	rm -rf Infra/MG.BuildingBlock.Infra.EF/bin/Release/

	echo "----------------------------- Remove release folder finiched -----------------------------"

	#Add git new tag
	git tag -a v$VER-alpha -m "v$VER-alpha" 

	echo "----------------------------- Add tag finiched -----------------------------"

	#Create Package of all projects
	dotnet pack Api/MG.BuildingBlock.Api/  -P:ContinuousIntegrationBuild=true
	dotnet pack Application/MG.BuildingBlock.Application/  -P:ContinuousIntegrationBuild=true
	dotnet pack Domain/MG.BuildingBlock.Domain/  -P:ContinuousIntegrationBuild=true
	dotnet pack Resources/MG.BuildingBlock.Resources/  -P:ContinuousIntegrationBuild=true
	dotnet pack Infra/MG.BuildingBlock.Infra/ -P:ContinuousIntegrationBuild=true
	dotnet pack Infra/MG.BuildingBlock.Infra.CrossCutting.Bus/ -P:ContinuousIntegrationBuild=true
	dotnet pack Infra/MG.BuildingBlock.Infra.CrossCutting.Identity/ -P:ContinuousIntegrationBuild=true
	dotnet pack Infra/MG.BuildingBlock.Infra.EF/ -P:ContinuousIntegrationBuild=true

	echo "----------------------------- Create Package finiched -----------------------------"

	#Push created package to Github nuget package registry 

	dotnet nuget push Api/MG.BuildingBlock.Api/bin/Release/MG.BuildingBlock.Api.$VER-alpha.nupkg --source "MG.BuildingBlocks" --skip-duplicate
	dotnet nuget push Application/MG.BuildingBlock.Application/bin/Release/MG.BuildingBlock.Application.$VER-alpha.nupkg --source "MG.BuildingBlocks" --skip-duplicate
	dotnet nuget push Domain/MG.BuildingBlock.Domain/bin/Release/MG.BuildingBlock.Domain.$VER-alpha.nupkg --source "MG.BuildingBlocks" --skip-duplicate
	dotnet nuget push Infra/MG.BuildingBlock.Infra/bin/Release/MG.BuildingBlock.Infra.$VER-alpha.nupkg --source "MG.BuildingBlocks" --skip-duplicate
	dotnet nuget push Infra/MG.BuildingBlock.Infra.CrossCutting.Bus/bin/Release/MG.BuildingBlock.Infra.CrossCutting.Bus.$VER-alpha.nupkg --source "MG.BuildingBlocks" --skip-duplicate
	dotnet nuget push Infra/MG.BuildingBlock.Infra.CrossCutting.Identity/bin/Release/MG.BuildingBlock.Infra.CrossCutting.Identity.$VER-alpha.nupkg --source "MG.BuildingBlocks" --skip-duplicate
	dotnet nuget push Infra/MG.BuildingBlock.Infra.EF/bin/Release/MG.BuildingBlock.Infra.EF.$VER-alpha.nupkg --source "MG.BuildingBlocks" --skip-duplicate
	dotnet nuget push Resources/MG.BuildingBlock.Resources/bin/Release/MG.BuildingBlock.Resources.$VER-alpha.nupkg --source "MG.BuildingBlocks" --skip-duplicate
	
       	echo "----------------------------- finished -----------------------------"

fi

