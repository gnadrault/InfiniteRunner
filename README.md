# IniniteRunner - SyntaxError

## Clonage du projet

⚠️ **Ne pas utiliser la fonctionnalité "Clone Repository" de Unity Hub.**
Ce projet utilise **Git LFS** pour les fichiers volumineux.

Le projet doit être cloné avec Git afin que Git LFS puisse récupérer correctement les assets.

```bash
git lfs install
git clone https://github.com/<utilisateur>/<repository>.git
cd <repository>
git lfs pull
```

Une fois le clonage terminé, ouvrir le dossier du projet avec Unity Hub.

L'utilisation du système de clonage intégré à Unity Hub peut entraîner l'absence des fichiers gérés par Git LFS (textures, modèles 3D, fichiers audio, etc.), provoquant des erreurs d'import et des assets manquants.