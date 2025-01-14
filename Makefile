#General vars
ARGS?=/restore /p:Configuration=Release
VS_PATH?=/Applications/Visual\ Studio\ \(Preview\).app
VS_DEBUG_PATH?=../vsmac/main/build/bin/VisualStudio.app

PROJECT_NAME=MonoDevelop.FeatureSwitch
PROJECT_VERSION=0.1

all:
	echo "Building $(PROJECT_NAME)..."
	msbuild /restore src/$(PROJECT_NAME).sln

clean:
	find . -type d -name bin -exec rm -rf {} \;
	find . -type d -name obj -exec rm -rf {} \;
	find . -type d -name packages -exec rm -rf {} \;

pack:
	mono $(VS_PATH)/Contents/MonoBundle/vstool.exe setup pack bin/$(PROJECT_NAME).dll

pack_debug:
	mono $(VS_DEBUG_PATH)/Contents/MonoBundle/vstool.exe setup pack bin/$(PROJECT_NAME).dll

install: pack
	mono $(VS_PATH)/Contents/MonoBundle/vstool.exe setup install ./$(PROJECT_NAME)_$(PROJECT_VERSION).mpack

install_debug: pack_debug
	mono $(VS_DEBUG_PATH)/Contents/MonoBundle/vstool.exe setup install ./$(PROJECT_NAME)_$(PROJECT_VERSION).mpack

.PHONY: all clean pack pack_debug install install_debug
