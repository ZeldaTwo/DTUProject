# DTUProject — GlassMetal

Logiciel de bureau Windows permettant le calcul des **épaisseurs de résistance (`eR`)** et de
**flèche (`eF`)** d'un vitrage, conformément au **DTU 39 (édition 2012)**.

L'outil couvre la chaîne complète de dimensionnement : à partir du contexte du chantier
(localisation, exposition au vent, à la neige, hauteur du bâtiment) et de la composition du
vitrage (monolithique, feuilleté, isolant 2 ou 3 faces), il détermine la pression appliquée,
l'épaisseur minimale requise `e1`, puis vérifie que la composition proposée satisfait les
critères de résistance et de flèche admissible.

---

## Sommaire

- [Contexte](#contexte)
- [Fonctionnement](#fonctionnement)
- [Architecture du dépôt](#architecture-du-dépôt)
- [Les trois couches](#les-trois-couches)
- [Le moteur de calcul en détail](#le-moteur-de-calcul-en-détail)
- [Données réglementaires embarquées](#données-réglementaires-embarquées)
- [Compilation et exécution](#compilation-et-exécution)
- [Le projet TestingCode](#le-projet-testingcode)
- [Limites connues et pistes d'évolution](#limites-connues-et-pistes-dévolution)
- [Glossaire](#glossaire)

---

## Contexte

Le DTU 39 fixe les règles françaises de calcul des épaisseurs de verre. Appliquer ces règles à la
main suppose de croiser plusieurs abaques (cartes de vent, zones de neige, catégories de terrain,
coefficients d'équivalence par type de glace), d'interpoler entre les valeurs tabulées, puis
d'enchaîner les vérifications. Ce logiciel automatise l'ensemble et affiche à chaque étape les
valeurs intermédiaires, de manière à ce que le résultat reste vérifiable.

Projet développé de juin à août 2024.

---

## Fonctionnement

L'application s'ouvre sur un écran d'accueil (logo + bouton **Commencer**) qui cède la place à la
fenêtre de travail unique, organisée en quatre quadrants.

### 1. Contexte du chantier → la pression `P`

| Saisie | Détail |
|---|---|
| Position du vitrage | en intérieur / en extérieur |
| Localisation | France métropolitaine / Outre-Mer |
| Inclinaison | vertical (β < 60°) / incliné |
| Région de vent | 1 à 4, ou Guadeloupe / Guyane / Martinique / Réunion |
| Catégorie de terrain | 0, II, IIIa, IIIb, IV (dans le rayon R) |
| Hauteur du vitrage | 6 tranches, de `H ≤ 9 m` à `H > 100 m` |

Si le vitrage est **incliné**, des champs supplémentaires apparaissent : altitude, zone de neige
(A1, A2, B1, B2, C1, C2, D, E ou « Pas de Neige »), coefficient de forme μ (0,8 / 1,6 / 2,2 / 2,8),
coefficients `Ce` et `Ct` (1,0 par défaut), épaisseur totale du vitrage, et éventuellement une
pression d'avalanche.

Des boutons d'aide **« ? »** ouvrent les cartes réglementaires (régions de vent, zones de neige,
catégories de terrain, coefficient μ, formes de vitrage) stockées dans `Images/`.

> Un champ **Pression Manuelle** permet de court-circuiter tout ce calcul et d'imposer directement
> une valeur : elle est alors prioritaire sur le calcul automatique.

### 2. Géométrie → les dimensions équivalentes `L` et `l`

Sept formes sont prises en charge. Les formes non rectangulaires sont ramenées à un **rectangle
équivalent** selon les formules du DTU :

| Forme | Saisie | Rectangle équivalent |
|---|---|---|
| Rectangle | `L`, `l` | tel quel |
| Triangle isocèle / rectangle | `a`, `b` | `⅔a` × `b` |
| Trapèze rectangle | `a`, `b`, `c` | `c + ⅔(a−c)` × `b` |
| Trapèze | `a`, `b`, `c`, `d` | `(a+c+d)/3` × `b` |
| Cercle | `a` | `0,85a` × `0,85a` |
| Demi-cercle + rectangle | `b`, `c` | `0,425b + c` × `b` |

Le logiciel affiche ensuite le rapport `l/L` et la surface, qui conditionnent la suite.

### 3. Mode d'appui → l'épaisseur minimale `e1`

Quatre modes de prise en feuillure :

- En appui sur toute la périphérie
- En appui sur 3 côtés
- En appui sur 2 côtés opposés
- En appui sur 2 côtés opposés avec maintien(s) ponctuel(s) sur les hauteurs

Selon le mode, on précise si le **bord libre** est le grand côté `L` ou le petit côté `l`, et
— pour le dernier mode — si le maintien est unique et centré ou s'il s'agit de deux maintiens
ponctuels équidistants (avec saisie de leur écartement).

### 4. Composition du vitrage → `eR`, `eF` et la flèche `f`

Quatre types de vitrage, chacun décliné en sous-compositions :

| Type | Sous-compositions |
|---|---|
| Monolithique | — |
| Feuilleté | 2 à 4+ composants |
| Isolant 2 faces | 2 monolithiques · monolithique + feuilleté · 2 feuilletés |
| Isolant 3 faces | 3 monolithiques · 2 monolithiques + feuilleté · monolithique + 2 feuilletés · 3 feuilletés |

Pour les vitrages isolants, les boutons **Précédent / Suivant** déroulent un assistant qui fait
saisir chaque couche successivement (type de glace parmi 20 références normalisées + épaisseur),
en mémorisant les valeurs afin de pouvoir revenir en arrière sans tout ressaisir.

Le bouton **Calcul de eR et eF** produit le verdict :

- si `eR < e1 × c` → l'épaisseur de résistance est insuffisante ;
- sinon la flèche est calculée et comparée aux limites admissibles ;
- si les deux conditions passent : *« La flèche et eR sont valides, le vitrage convient »*.

L'application affiche également `α`, la flèche obtenue, et une **désignation synthétique** du
vitrage (ex. `44.x/.../8 trempé`).

---

## Architecture du dépôt

```
DTUProject/
├── README.md
└── GlassMetalProj/                        Solution Visual Studio (2 projets)
    ├── GlassMetalProj.sln
    │
    ├── GlassMetalProj/                    ── Application WPF ──
    │   ├── App.xaml(.cs)                  Point d'entrée, thème MahApps.Metro, styles globaux
    │   ├── MainWindow.xaml(.cs)           Écran d'accueil (logo + bouton « Commencer »)
    │   ├── WorkingWindow.xaml(.cs)        Écran principal — 490 l. XAML / 2 055 l. C#
    │   ├── HelperWindow.xaml(.cs)         Fenêtre d'aide : une image plein cadre
    │   ├── HelperWindowMultiImages.xaml(.cs)  Fenêtre d'aide : galerie d'images
    │   ├── FilledInfos.cs                 État de la saisie + abaques du DTU (709 l.)
    │   ├── MathsHelper.cs                 Moteur de calcul (864 l.)
    │   ├── Images/                        Cartes vent / neige / terrain, formes, logo
    │   └── packages.config                MahApps.Metro 2.4.10, ControlzEx 4.4.0
    │
    └── TestingCode/                       ── Console de vérification manuelle ──
        └── Program.cs                     Jeux d'essai sur eR et α (179 l.)
```

**Stack technique :** C# · .NET Framework 4.7.2 · WPF · MahApps.Metro 2.4.10 · ControlzEx 4.4.0 ·
Microsoft.Xaml.Behaviors.Wpf 1.1.19 — gestion des paquets via `packages.config` (NuGet historique).

---

## Les trois couches

### `FilledInfos.cs` — données de saisie et abaques

Cette classe joue deux rôles : elle porte l'état de tout ce que l'utilisateur a saisi, et elle
embarque les tables réglementaires du DTU. Ses méthodes principales :

| Méthode | Rôle |
|---|---|
| `CalculateDimensions(i, a, b, c, d)` | Ramène une forme quelconque au rectangle équivalent `L × l` |
| `CalculatePressure(...)` | Détermine la pression de calcul (vent seul, ou combinaison vent/neige/avalanche) |
| `FindS1andS2(...)` | Charges de neige normale et accidentelle, avec extrapolation en altitude |
| `findAlpha(type, b)` | Coefficient α de flèche, interpolé entre les valeurs tabulées |
| `LinearRegression(...)` | Interpolation linéaire entre deux points d'abaque |

### `MathsHelper.cs` — le moteur

Ne dépend que de `FilledInfos`, qui lui est injecté au constructeur.

| Méthode | Rôle |
|---|---|
| `e1Calculatation(type, tag, tag2)` | Épaisseur minimale selon le mode d'appui |
| `eRCalculation(...)` | Épaisseur de résistance (~330 lignes, tous types de vitrage) |
| `eFCalculation(...)` | Épaisseur de flèche |
| `fCalculation(...)` | Flèche `f` + vérification des critères d'admissibilité |
| `eRisValid()` | Contrôle final `eR ≥ e1 × c` |
| `SummaryThickness(...)` | Désignation commerciale du vitrage |

### `WorkingWindow.xaml.cs` — l'interface

Fenêtre unique 1100×650 non redimensionnable, découpée visuellement en quatre quadrants par des
`GridSplitter` décoratifs (désactivés), tous les contrôles étant positionnés en marges absolues.

L'essentiel du code-behind gère la **révélation progressive** des contrôles : chaque case cochée
ou élément sélectionné affiche ou masque les champs devenus pertinents, de façon à ne jamais
présenter à l'utilisateur un formulaire complet dont 80 % serait hors sujet. `CheckEverythingIsChecked()`
valide que toutes les informations nécessaires sont présentes avant de lancer le calcul de pression.

À noter : `Preview_Text_Input` force la **virgule** comme séparateur décimal, conformément à
l'usage français.

---

## Le moteur de calcul en détail

### Épaisseur minimale `e1`

`P` est la pression en Pa, `L` et `l` les dimensions en mètres.

| Mode d'appui | Condition | Formule |
|---|---|---|
| Toute la périphérie | `L/l ≤ 2,5` | `e1 = √(P·L·l / 100)` |
| Toute la périphérie | `L/l > 2,5` | `e1 = l·√P / 6,3` |
| 3 côtés, bord libre = `L` | `L/l ≤ 7,5` | `e1 = √(3·L·l·P / 100)` |
| 3 côtés, bord libre = `L` | `L/l > 7,5` | `e1 = 3·l·√P / 6,3` |
| 3 côtés, bord libre = `l` | — | `e1 = l·√P / 6,3` |
| 2 côtés opposés | — | `e1 = b·√P / 6,3` |
| 2 côtés + 1 maintien centré | — | `e1 = (b·√P / 6,3) × 0,625` |
| 2 côtés + 2 maintiens équidistants | — | `e1 = (b·√P / 6,3) × 0,588` |

### Épaisseur de résistance `eR`

Le principe est constant quel que soit le type de vitrage : **sommer les épaisseurs des couches,
diviser par le produit des coefficients d'équivalence**, puis s'assurer que le résultat n'est
jamais inférieur à la plus épaisse des couches individuelles.

| Type | Formule |
|---|---|
| Monolithique | `eR = e / ε₃` |
| Feuilleté | `eR = Σeᵢ / (0,9 · ε₂ · ε₃ₘₐₓ)` |
| Isolant 2 faces | `eR = Σ(couches) / (0,9 · ε₁ · ε₃ₘₐₓ)` |
| Isolant 3 faces | idem avec `ε₁` de l'isolant 3 faces |

Pour les compositions mixtes (monolithique + feuilleté, double feuilleté…), chaque feuilleté est
d'abord réduit à son épaisseur équivalente via son propre `ε₂`, avant que l'ensemble ne soit
divisé par `ε₁`. Le `ε₃` retenu est **le maximum** sur toutes les glaces de la composition.

### Flèche `f`

```
f = α × (P / 1,5) × b⁴ / eF³
```

où `α` provient de l'abaque interpolée et `b` est la dimension déterminante (bord libre ou petit côté).

**Critères d'admissibilité :**

| Mode d'appui | Critère de flèche | Plafond absolu |
|---|---|---|
| 4 côtés | `f ≤ b/60` | 30 mm |
| 3 côtés / 2 côtés, simple vitrage | `f ≤ b/100` | 50 mm |
| 3 côtés / 2 côtés, double vitrage | `f ≤ b/150` | 50 mm |
| 2 côtés + maintiens ponctuels | `f ≤ b/60` **et** `f₂` selon le critère ci-dessus | 30 puis 50 mm |

> ⚠️ **Piège de lecture dans le code :** ces critères sont écrits dans des unités différentes.
> Pour le cas 4 côtés, `b` est converti en millimètres puis testé contre `b/60`. Pour les autres
> cas, `b` reste en mètres et le test s'écrit `b*10` ou `b*6.67` — ce qui correspond bien à
> `b_mm/100` et `b_mm/150`. Les trois critères sont donc homogènes malgré leur écriture.

---

## Données réglementaires embarquées

Toutes les abaques du DTU sont codées en dur dans `FilledInfos.cs` :

| Donnée | Structure | Contenu |
|---|---|---|
| Pressions de vent | `windPressure[40,5]` | 8 régions × 5 catégories de terrain × 5 tranches de hauteur. Accès par `IndexRegion × 5 + IndexFieldType` |
| Neige au sol (< 200 m) | `SnowChargeBelow200[8]` | 450 à 1400 Pa selon la zone |
| Neige accidentelle | `SnowChargeAD[8]` | 0 à 1800 Pa selon la zone |
| `ε₁` — isolant | `equivalencefactor1[2]` | 1,6 (2 faces) · 2,0 (3 faces) |
| `ε₂` — feuilleté | `equivalencefactor2[5]` | 1,3 à 2,0 selon le nombre de composants |
| `ε₃` — monolithique | `equivalencefactor3[20]` | 0,55 à 1,4 selon le type de glace |
| `α` — flèche | `alphafactor[33]` | Tabulé par rapport `l/L`, interpolé linéairement |

**Extrapolation de la charge de neige en altitude** (`FindS1andS2`), par paliers :

| Altitude | ΔS (zone E) | ΔS (autres zones) |
|---|---|---|
| ≤ 200 m | 0 | 0 |
| 200 – 500 m | `1,5·A − 300` | `A − 200` |
| 500 – 1000 m | `3,5·A − 1300` | `1,5·A − 450` |
| 1000 – 2000 m | `7·A − 4800` | `3,5·A − 2450` |
| > 2000 m | *non couvert — renvoi au DPM* | idem |

Les 20 types de glace monolithique correspondent aux références normalisées : recuit (NF EN 572-2),
recuit armé, étiré, imprimé, imprimé armé, trempé (NF EN 12150 / 14179), émaillé trempé, imprimé
trempé, durci (NF EN 1863), borosilicate (NF EN 1748-1), borosilicate trempé, émaillé durci,
alcalino-terreux recuit et trempé, vitrocéramique, trempé chimique, dépolis (acide, sablage,
grenaillage) et gravé.

---

## Compilation et exécution

### Prérequis

- **Windows** (voir la note ci-dessous)
- Visual Studio 2022 (v17.10 ou ultérieur) avec la charge de travail *Développement .NET Desktop*,
  ou les *Build Tools* seuls
- .NET Framework 4.7.2 Developer Pack

### Build

```bash
# Restauration des paquets NuGet (packages.config)
nuget restore GlassMetalProj/GlassMetalProj.sln

# Compilation
msbuild GlassMetalProj/GlassMetalProj.sln /p:Configuration=Release
```

Ou simplement : ouvrir `GlassMetalProj/GlassMetalProj.sln` dans Visual Studio et lancer la
génération. Le projet de démarrage est `GlassMetalProj`.

### ⚠️ Note sur Linux et macOS

**Le projet ne compile pas hors de Windows.** WPF repose sur des composants natifs Win32/DirectX ;
Mono ne l'a jamais implémenté, et même en .NET moderne le SDK `WindowsDesktop` exige Windows pour
la compilation, pas seulement pour l'exécution. Une machine virtuelle Windows est la voie fiable.

En revanche, le **cœur métier est portable** : `FilledInfos.cs` et `MathsHelper.cs` (≈ 1 570 lignes)
ne touchent à Windows que par un unique appel `MessageBox.Show` ([FilledInfos.cs:228](GlassMetalProj/GlassMetalProj/FilledInfos.cs)),
les `using System.Windows`, `System.Web.Hosting` et `System.Runtime.InteropServices` en tête de
fichier étant par ailleurs inutilisés. Extraire ces deux fichiers dans une bibliothèque .NET 8
permettrait de compiler et tester le calcul sur n'importe quelle plateforme.

---

## Le projet TestingCode

`TestingCode` est une **console de vérification manuelle**, pas une suite de tests automatisés :
les valeurs attendues sont écrites en dur et les résultats s'affichent sous forme de `True` / `False`
à l'écran, à lire soi-même. Une partie des cas est commentée.

Cas couverts :

- `eR` monolithique — épaisseur 5 mm, glace recuite, attendu : 5
- `eR` feuilleté — 8 + 8 mm trempé, attendu : 22,4
- `eR` isolant 2 faces, double monolithique
- `α` pour les trois types de maintien, sur 12 couples `(l, L)`

Aucun framework de test n'est présent dans le dépôt.

---

## Limites connues et pistes d'évolution

### Limites

- **Pas de persistance** : ni sauvegarde d'un dossier d'étude, ni export PDF ou impression d'une
  note de calcul — sortie pourtant habituellement attendue d'un tel outil.
- **Abaques non externalisées** : les constructeurs `FilledInfos(pathPressure, pathFactors)`
  acceptent des chemins de fichiers, mais l'initialisation ne s'effectue que
  `if (path == string.Empty)`. L'intention de charger les tables depuis des fichiers externes est
  présente mais non implémentée : toute mise à jour du DTU impose aujourd'hui une recompilation.
- **Altitudes > 2000 m** non couvertes (renvoi explicite au DPM).
- **Pas de tests automatisés.**

### Points à vérifier dans le code

- `CalculateDimensions`, cas 6 (demi-cercle + rectangle) : la condition `if (b > 0.425*b + c)`
  compare `b` à une expression de `b` — un autre paramètre était probablement attendu.
- `CalculateDimensions`, cas 3 (trapèze rectangle) : l'affectation `L = l = c + ⅔(a−c)` est
  immédiatement écrasée par `l = b` à la ligne suivante.
- Plusieurs commentaires `//TODO - check if eR is less than the thicknesses` subsistent alors que
  le code correspondant semble présent — vraisemblablement des reliquats.

### Évolutions envisageables

1. Extraire `FilledInfos` + `MathsHelper` dans une bibliothèque `GlassMetal.Core` multiplateforme.
2. Convertir `TestingCode` en véritable suite **xUnit**, en transformant les valeurs attendues
   existantes en assertions.
3. Externaliser les abaques du DTU en JSON ou CSV, comme le prévoyaient les constructeurs.
4. Ajouter l'export d'une note de calcul (PDF) et la sauvegarde des dossiers d'étude.
5. À plus long terme, porter l'interface sur **Avalonia UI** (XAML très proche de WPF, fonctionne
   sous Linux, macOS et Windows) — un chantier conséquent vu la taille du code-behind.

---

## Glossaire

| Terme | Signification |
|---|---|
| **DTU** | Document Technique Unifié — règles de l'art de la construction en France |
| **DPM** | Documents Particuliers du Marché |
| **`e1`** | Épaisseur minimale requise par le mode d'appui et la pression |
| **`eR`** | Épaisseur de résistance de la composition proposée |
| **`eF`** | Épaisseur équivalente utilisée pour le calcul de flèche |
| **`f`** | Flèche du vitrage sous charge |
| **`L` / `l`** | Grande et petite dimension du rectangle équivalent |
| **`ε₁` / `ε₂` / `ε₃`** | Coefficients d'équivalence : isolant / feuilleté / monolithique |
| **`α`** | Coefficient de flèche, fonction du rapport `l/L` et du mode d'appui |
| **`μ`, `Ce`, `Ct`** | Coefficients de forme, d'exposition et thermique pour la charge de neige |
| **Feuillure** | Rainure périphérique dans laquelle le vitrage est engagé |
| **Bord libre** | Côté du vitrage non tenu en feuillure |
| **Monolithique** | Vitrage constitué d'une seule glace |
| **Feuilleté** | Plusieurs glaces assemblées par intercalaires (PVB) |
| **Isolant** | Vitrage à lame d'air, 2 ou 3 composants séparés |
