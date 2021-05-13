## What is BimAngle Engine SDK?

BimAngle Engine can export your model to other formats, such as **Autodesk Forge Viewer SVF/F2D**, **glTF/glb**, **Cesium 3D Tiles**, etc. No need to rely on other online services, can be run offline.

BimAngle Engine has four versions so far, namely **RVT**, **NW** ,**DWG**, **DGN** and **SKP**.

#### BimAngle Engine RVT
The runtime environment is **Autodesk Revit** addin, support Autodesk Revit 2014-2021.

#### BimAngle Engine NW
The runtime environment is **Autodesk Navisworks** addin, support Autodesk Navisworks Manager 2014-2021.

#### BimAngle Engine DWG
For *.dwg(AutoCAD drawing), no other software dependencies are required.

#### BimAngle Engine DGN
The runtime environment is **Bentley MicroStation CE** addin.

#### BimAngle Engine SKP
For *.skp(SketchUp model), no other software dependencies are required.

## Quick start

* Clone or download this repository
* Depending on the version you choose, use **Visual Studio 2017** to open the corresponding solution file(*.sln)
* Copy all nuget scripts from **NugetCommands.txt**, paste to VS2017 **(Menu)Tools** -> **NuGet Package Manager** -> **Package Manager Console**, and run these nuget scripts, this step will ensure the "BimAngle Engine SDK" available
* Rebuild the solution
* for BimAngle Engine **RVT**, run Revit and load model, you can find export button in toolbar.
* for BimAngle Engine **NW**, run Navisworks and load model, you can find export button in toolbar.
* for BimAngle Engine **DWG**, just run it
* for BimAngle Engine **DGN**, run MicroStation CE and load model, you can find export button in toolbar.
* for BimAngle Engine **SKP**, just run it

## How to purchase licenses?
Please contact liuyongsheng@msn.com


