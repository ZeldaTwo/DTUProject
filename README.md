# DTUProject — GlassMetal

A Windows desktop application that calculates the **resistance thickness** and **deflection**
of a glazing unit, in accordance with the French **DTU 39 standard (2012 edition)**.

---

## Overview

Sizing a glazing unit under DTU 39 means cross-referencing several regulatory charts — wind
region maps, snow zones, terrain categories, equivalence coefficients specific to each type of
glass — interpolating between tabulated values, and then running a series of checks.

GlassMetal automates that work. Given the site conditions and the intended glazing composition,
it states whether that composition is suitable, and displays the intermediate values at every
step so the result stays verifiable.

---

## Usage

The application opens on a welcome screen, then presents a single working window where four
groups of information are entered in turn.

### 1. Site

Glazing position (indoor or outdoor), location (mainland France or overseas territories), tilt,
wind region, terrain category and glazing height.

For a tilted glazing unit, additional fields appear: altitude, snow zone, shape and exposure
coefficients, and optionally an avalanche pressure.

From these, the application derives the **pressure applied to the glazing**. A pressure can also
be entered manually to bypass this calculation.

### 2. Geometry

Seven shapes are supported: rectangle, isosceles triangle, right triangle, right trapezoid,
trapezoid, circle, and a semicircle above a rectangle.

Non-rectangular shapes are automatically reduced to an **equivalent rectangle**, whose dimensions,
aspect ratio and surface area are displayed.

### 3. Support

Four mounting configurations are available: supported along the full perimeter, on three sides,
on two opposite sides, or on two opposite sides with point supports. Depending on the case, you
specify which side is the free edge and where the point supports are located.

The application then computes the **minimum required thickness**.

### 4. Composition

Four glazing families are handled — monolithic, laminated, double insulating and triple
insulating — each available in several compositions (double monolithic, monolithic + laminated,
triple laminated, and so on).

For insulating units, a step-by-step assistant collects each layer in turn: the type of glass,
chosen from around twenty standardised references, and its thickness.

The final calculation reports whether the glazing is suitable, or which condition fails.

> Throughout the form, **"?"** buttons display the DTU charts: wind regions, snow zones, terrain
> categories, coefficients and glazing shapes.

---

## Architecture

A **WPF** application written in **C#** on **.NET Framework 4.7.2**, styled with the
**MahApps.Metro** UI library.

The project separates the interface from the calculation:

| Component | Role |
|---|---|
| `MainWindow` | Welcome screen |
| `WorkingWindow` | Main working window, where all input is collected |
| `HelperWindow` | Help windows displaying the charts and maps |
| `FilledInfos` | User input and the DTU regulatory tables |
| `MathsHelper` | Calculation engine (pressure, thicknesses, deflection) |

The DTU charts — wind pressures by region and exposure, snow loads by zone and altitude,
equivalence coefficients by glass type — are embedded in the application, which therefore runs
without any network connection or external data file.

### Layout

```
DTUProject/
└── GlassMetalProj/              Visual Studio solution
    ├── GlassMetalProj/          Application
    │   └── Images/              DTU charts and maps, logo
    └── TestingCode/             Calculation check console
```

---

## Setup

### Requirements

- **Windows**
- **Visual Studio 2022** (17.10 or later) with the *.NET Desktop Development* workload
- **.NET Framework 4.7.2 Developer Pack**

### Packages

The project uses three NuGet packages: MahApps.Metro, ControlzEx and Microsoft.Xaml.Behaviors.Wpf.

Visual Studio restores them automatically when the solution is opened. Otherwise, from the
repository root:

```
nuget restore GlassMetalProj/GlassMetalProj.sln
```

### Build

Open `GlassMetalProj/GlassMetalProj.sln` in Visual Studio, make sure the startup project is
**GlassMetalProj**, then build the solution (`Ctrl+Shift+B`).

From the command line:

```
msbuild GlassMetalProj/GlassMetalProj.sln /p:Configuration=Release
```

### Run

Press `F5` in Visual Studio to launch the application.

Otherwise the executable is located in `GlassMetalProj/GlassMetalProj/bin/Release/`. The `Images`
folder must remain alongside the executable for the help charts to display.

---

## Glossary

| Term | Meaning |
|---|---|
| **DTU** | *Document Technique Unifié* — French building construction standards |
| **Resistance thickness** | Equivalent thickness of the glazing with respect to mechanical strength |
| **Deflection** | Deformation of the glazing under the applied load |
| **Rebate** | Peripheral groove into which the glazing is set |
| **Free edge** | Edge of the glazing not held in a rebate |
| **Monolithic** | Glazing made of a single pane |
| **Laminated** | Several panes bonded together by interlayers |
| **Insulating** | Glazing with an air gap, with 2 or 3 separate components |
