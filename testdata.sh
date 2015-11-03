
curl -H 'Content-Type: application/json' -X PUT -d '{"key":"create-candidate","name":"Create Candidate","description":"Allows the user to create candidates"}' http://localhost:4444/api/permissions
curl -H 'Content-Type: application/json' -X PUT -d '{"key":"archive-candidate","name":"Archive Candidate","description":"Allows the user to archive candidates"}' http://localhost:4444/api/permissions
curl -H 'Content-Type: application/json' -X PUT -d '{"key":"restore-candidate","name":"Restore Candidate","description":"Allows the user to restore archived candidates"}' http://localhost:4444/api/permissions
curl -H 'Content-Type: application/json' -X PUT -d '{"key":"delete-candidate","name":"Delete Candidate","description":"Allows the user to delete candidate"}' http://localhost:4444/api/permissions

curl -H 'Content-Type: application/json' -X PUT -d '{"key":"db-cleaner","name":"Database Cleaner","description":"Able to archive and restore candidates"}' http://localhost:4444/api/roles
curl -d "" -X PUT http://localhost:4444/api/roles/db-cleaner/addPermission/archive-candidate
curl -d "" -X PUT http://localhost:4444/api/roles/db-cleaner/addPermission/restore-candidate

curl -H 'Content-Type: application/json' -X PUT -d '{"key":"admin","name":"Administrator","description":"DO ALL THE THINGS!"}' http://localhost:4444/api/roles
curl -d "" -X PUT http://localhost:4444/api/roles/db-cleaner/addPermission/archive-candidate
curl -d "" -X PUT http://localhost:4444/api/roles/db-cleaner/addPermission/restore-candidate
curl -d "" -X PUT http://localhost:4444/api/roles/db-cleaner/addPermission/create-candidate
curl -d "" -X PUT http://localhost:4444/api/roles/db-cleaner/addPermission/delete-candidate

curl -H 'Content-Type: application/json' -X PUT -d '{"key":"00001","name":"Andy Dote"}' http://localhost:4444/api/users
curl -d "" -X PUT http://localhost:4444/api/users/00001/addRole/db-cleaner
curl -d "" -X PUT http://localhost:4444/api/users/00001/addRole/admin
curl -d "" -X PUT http://localhost:4444/api/users/00001/addPermission/create-candidate
