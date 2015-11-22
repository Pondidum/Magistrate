
curl -s  -w "%{http_code}\r\n" -H 'Content-Type: application/json' -X PUT -d '{"key":"create-candidate","name":"Create Candidate","description":"Allows the user to create candidates"}' http://localhost:4444/api/permissions
curl -s  -w "%{http_code}\r\n" -H 'Content-Type: application/json' -X PUT -d '{"key":"archive-candidate","name":"Archive Candidate","description":"Allows the user to archive candidates"}' http://localhost:4444/api/permissions
curl -s  -w "%{http_code}\r\n" -H 'Content-Type: application/json' -X PUT -d '{"key":"restore-candidate","name":"Restore Candidate","description":"Allows the user to restore archived candidates"}' http://localhost:4444/api/permissions
curl -s  -w "%{http_code}\r\n" -H 'Content-Type: application/json' -X PUT -d '{"key":"delete-candidate","name":"Delete Candidate","description":"Allows the user to delete candidate"}' http://localhost:4444/api/permissions

curl -s  -w "%{http_code}\r\n" -H 'Content-Type: application/json' -X PUT -d '{"key":"db-cleaner","name":"Database Cleaner","description":"Able to archive and restore candidates"}' http://localhost:4444/api/roles
curl -s  -w "%{http_code}\r\n" -d "" -X PUT http://localhost:4444/api/roles/db-cleaner/permission/archive-candidate
curl -s  -w "%{http_code}\r\n" -d "" -X PUT http://localhost:4444/api/roles/db-cleaner/permission/restore-candidate

curl -s  -w "%{http_code}\r\n" -H 'Content-Type: application/json' -X PUT -d '{"key":"admin","name":"Administrator","description":"DO ALL THE THINGS!"}' http://localhost:4444/api/roles
curl -s  -w "%{http_code}\r\n" -d "" -X PUT http://localhost:4444/api/roles/admin/permission/archive-candidate
curl -s  -w "%{http_code}\r\n" -d "" -X PUT http://localhost:4444/api/roles/admin/permission/restore-candidate
curl -s  -w "%{http_code}\r\n" -d "" -X PUT http://localhost:4444/api/roles/admin/permission/create-candidate
curl -s  -w "%{http_code}\r\n" -d "" -X PUT http://localhost:4444/api/roles/admin/permission/delete-candidate

curl -s  -w "%{http_code}\r\n" -H 'Content-Type: application/json' -X PUT -d '{"key":"00001","name":"Andy Dote"}' http://localhost:4444/api/users
curl -s  -w "%{http_code}\r\n" -d "" -X PUT http://localhost:4444/api/users/00001/role/db-cleaner
curl -s  -w "%{http_code}\r\n" -d "" -X PUT http://localhost:4444/api/users/00001/role/admin
curl -s  -w "%{http_code}\r\n" -d "" -X PUT http://localhost:4444/api/users/00001/include/create-candidate
