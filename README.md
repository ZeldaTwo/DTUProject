# DTUProject — GlassMetal

Logiciel de bureau Windows permettant le calcul des **épaisseurs de résistance** et de **flèche**
d'un vitrage, conformément au **DTU 39 (édition 2012)**.

---

## À quoi sert le logiciel

Dimensionner un vitrage selon le DTU 39 suppose de croiser plusieurs abaques réglementaires
(cartes des régions de vent, zones de neige, catégories de terrain, coefficients d'équivalence
propres à chaque type de glace), d'interpoler entre les valeurs tabulées, puis d'enchaîner
une série de vérifications.

GlassMetal automatise ce travail. À partir des caractéristiques du chantier et de la composition
du vitrage envisagée, il indique si cette composition convient, et affiche les valeurs
intermédiaires à chaque étape pour que le résultat reste vérifiable.

---

## Comment on l'utilise

L'application s'ouvre sur un écran d'accueil, puis présente une fenêtre de travail unique
où l'on renseigne successivement quatre groupes d'informations.

### 1. Le chantier

Position du vitrage (intérieur ou extérieur), localisation (France métropolitaine ou Outre-Mer),
inclinaison, région de vent, catégorie de terrain et hauteur du vitrage.

Pour un vitrage incliné, des champs complémentaires apparaissent : altitude, zone de neige,
coefficients de forme et d'exposition, et éventuellement une pression d'avalanche.

Le logiciel en déduit la **pression appliquée au vitrage**. Une pression peut aussi être saisie
manuellement pour court-circuiter ce calcul.

### 2. La géométrie

Sept formes sont prises en charge : rectangle, triangle isocèle, triangle rectangle, trapèze
rectangle, trapèze, cercle, et demi-cercle surmontant un rectangle.

Les formes non rectangulaires sont automatiquement ramenées à un **rectangle équivalent**,
dont le logiciel affiche les dimensions, le rapport de forme et la surface.

### 3. Le mode de pose

Quatre modes de prise en feuillure sont proposés : appui sur toute la périphérie, sur trois côtés,
sur deux côtés opposés, ou sur deux côtés opposés avec maintiens ponctuels. On précise selon
le cas quel côté constitue le bord libre et où se situent les maintiens.

Le logiciel calcule alors l'**épaisseur minimale requise**.

### 4. La composition du vitrage

Quatre familles de vitrage sont gérées — monolithique, feuilleté, isolant 2 faces et isolant
3 faces — chacune déclinée en plusieurs compositions possibles (double monolithique,
monolithique + feuilleté, triple feuilleté, etc.).

Pour les vitrages isolants, un assistant fait saisir chaque couche l'une après l'autre :
type de glace choisi parmi une vingtaine de références normalisées, et épaisseur.

Le calcul final indique si le vitrage convient, ou bien quelle condition n'est pas respectée.

> Tout au long de la saisie, des boutons **« ? »** affichent les cartes et abaques du DTU :
> régions de vent, zones de neige, catégories de terrain, coefficients, formes de vitrage.

---

## Comment c'est fait

Application **WPF** écrite en **C#** sur **.NET Framework 4.7.2**, habillée par la bibliothèque
d'interface **MahApps.Metro**.

Le projet sépare l'interface du calcul :

| Élément | Rôle |
|---|---|
| `MainWindow` | Écran d'accueil |
| `WorkingWindow` | Fenêtre de travail principale, où se fait toute la saisie |
| `HelperWindow` | Fenêtres d'aide affichant les cartes et abaques |
| `FilledInfos` | Informations saisies et tables réglementaires du DTU |
| `MathsHelper` | Moteur de calcul (pression, épaisseurs, flèche) |

Les abaques du DTU — pressions de vent par région et par exposition, charges de neige par zone
et par altitude, coefficients d'équivalence par type de glace — sont intégrées au logiciel,
qui fonctionne donc sans connexion ni fichier de données externe.

### Structure du dépôt

```
DTUProject/
└── GlassMetalProj/              Solution Visual Studio
    ├── GlassMetalProj/          Application
    │   └── Images/              Cartes et abaques du DTU, logo
    └── TestingCode/             Console de vérification des calculs
```

---

## Installation et exécution

### Prérequis

- **Windows**
- **Visual Studio 2022** (17.10 ou ultérieur) avec la charge de travail
  *Développement .NET Desktop*
- **.NET Framework 4.7.2 Developer Pack**

### Installation des paquets

Le projet utilise trois paquets NuGet : MahApps.Metro, ControlzEx et Microsoft.Xaml.Behaviors.Wpf.

Dans Visual Studio, ils sont restaurés automatiquement à l'ouverture de la solution.
Sinon, en ligne de commande depuis la racine du dépôt :

```
nuget restore GlassMetalProj/GlassMetalProj.sln
```

### Compilation

Ouvrir `GlassMetalProj/GlassMetalProj.sln` dans Visual Studio, vérifier que le projet de
démarrage est bien **GlassMetalProj**, puis lancer la génération (`Ctrl+Maj+B`).

En ligne de commande :

```
msbuild GlassMetalProj/GlassMetalProj.sln /p:Configuration=Release
```

### Exécution

Depuis Visual Studio, `F5` lance l'application.

Sinon, l'exécutable se trouve dans `GlassMetalProj/GlassMetalProj/bin/Release/`.
Le dossier `Images` doit rester à côté de l'exécutable pour que les cartes d'aide s'affichent.

---

## Glossaire

| Terme | Signification |
|---|---|
| **DTU** | Document Technique Unifié — règles de l'art de la construction en France |
| **Épaisseur de résistance** | Épaisseur équivalente du vitrage vis-à-vis de sa tenue mécanique |
| **Flèche** | Déformation du vitrage sous la charge appliquée |
| **Feuillure** | Rainure périphérique dans laquelle le vitrage est engagé |
| **Bord libre** | Côté du vitrage non tenu en feuillure |
| **Monolithique** | Vitrage constitué d'une seule glace |
| **Feuilleté** | Plusieurs glaces assemblées par intercalaires |
| **Isolant** | Vitrage à lame d'air, à 2 ou 3 composants séparés |
