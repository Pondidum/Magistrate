
curl -s  -w "%{http_code}\r\n" -H 'Content-Type: application/json' -X PUT -d '{"key":"create-candidate","name":"Create Candidate","description":"Allows the user to create candidates"}' http://localhost:4444/api/permissions
curl -s  -w "%{http_code}\r\n" -H 'Content-Type: application/json' -X PUT -d '{"key":"archive-candidate","name":"Archive Candidate","description":"Allows the user to archive candidates"}' http://localhost:4444/api/permissions
curl -s  -w "%{http_code}\r\n" -H 'Content-Type: application/json' -X PUT -d '{"key":"restore-candidate","name":"Restore Candidate","description":"Allows the user to restore archived candidates"}' http://localhost:4444/api/permissions
curl -s  -w "%{http_code}\r\n" -H 'Content-Type: application/json' -X PUT -d '{"key":"delete-candidate","name":"Delete Candidate","description":"Allows the user to delete candidate"}' http://localhost:4444/api/permissions

curl -s  -w "%{http_code}\r\n" -H 'Content-Type: application/json' -X PUT -d '{"key":"db-cleaner","name":"Database Cleaner","description":"Able to archive and restore candidates"}' http://localhost:4444/api/roles
curl -s  -w "%{http_code}\r\n" -H 'Content-Type: application/json' -X PUT -d '["archive-candidate"]' http://localhost:4444/api/roles/db-cleaner/permissions
curl -s  -w "%{http_code}\r\n" -H 'Content-Type: application/json' -X PUT -d '["restore-candidate"]' http://localhost:4444/api/roles/db-cleaner/permissions

curl -s  -w "%{http_code}\r\n" -H 'Content-Type: application/json' -X PUT -d '{"key":"admin","name":"Administrator","description":"DO ALL THE THINGS!"}' http://localhost:4444/api/roles
curl -s  -w "%{http_code}\r\n" -H 'Content-Type: application/json' -X PUT -d '["archive-candidate"]' http://localhost:4444/api/roles/admin/permissions
curl -s  -w "%{http_code}\r\n" -H 'Content-Type: application/json' -X PUT -d '["restore-candidate"]' http://localhost:4444/api/roles/admin/permissions
curl -s  -w "%{http_code}\r\n" -H 'Content-Type: application/json' -X PUT -d '["create-candidate"]' http://localhost:4444/api/roles/admin/permissions
curl -s  -w "%{http_code}\r\n" -H 'Content-Type: application/json' -X PUT -d '["delete-candidate"]' http://localhost:4444/api/roles/admin/permissions

curl -s  -w "%{http_code}\r\n" -H 'Content-Type: application/json' -X PUT -d '{"key":"00001","name":"Andy Dote"}' http://localhost:4444/api/users
curl -s  -w "%{http_code}\r\n" -H 'Content-Type: application/json' -X PUT -d '["db-cleaner"]' http://localhost:4444/api/users/00001/roles
curl -s  -w "%{http_code}\r\n" -H 'Content-Type: application/json' -X PUT -d '["admin"]' http://localhost:4444/api/users/00001/roles
curl -s  -w "%{http_code}\r\n" -H 'Content-Type: application/json' -X PUT -d '["create-candidate"]' http://localhost:4444/api/users/00001/includes
