
Flujo de trabajo Ossmmasoft

Si no existe la rama develop en local, la traemos del remoto
    git checkout -b develop origin/develop

Si existe en el local, nos colocamos en develop
    git checkout develop

Descargar todas las ramas
    git pull --all

Una vez tengamos el nombre de la tarea asignada en JIRA debemos crear la
rama en nuestro git local, para ellos usamos esto, crea la rama y nos mueve a la nueva rama
    git checkout -b <Tarea de jira>
    
