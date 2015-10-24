

# aggregates

* Permission
  - key: `string` (url safe)
  - name: `string`
  - description: `string`
  - `CreateNew(key, name, description)`
  - `UpdateDescription(description)`
* Role
  - key: `string` (url safe)
  - name: `string`
  - description: `string`
  - permissions: `hash<permission>`
  - `AddPermission(permission)`
  - `RemovePermission(permission)`
* User
  - key: `string` (url safe)
  - name: `string`
  - roles: `hash<string>`
  - includes: `hash<string>`
  - revokes: `hash<string>`
  - `AddPermission(permission)`
  - `RemovePermission(permission)`
  - `AddRole(role)`
  - `RemoveRole(role)`

# Notes

* Implement as an owin middleware
* Reactjs frontent (`React.Owin` to host)
* Hooks:
  * On Permisson Effect Changed:
    * Role has permission added/remvoed
    * User has permission added/removed
    * User has role added/removed
* Authentication
  * User specified Owin middleware?
  * Use own permission/role?
  * permissions:
    * create|view|edit|delete permission
    * create|view|edit|delete role
    * create|view|edit|delete user
  * roles:
    * admin: can do any
    * viewer: can view anything
    * author: can edit descriptions?

# Usage

* api
  * http/get/api/permissions/all
  * http/get/api/permissions/{permission-key}
  * http/get/api/roles/all
  * http/get/api/roles/{role-key}
  * http/get/api/user/all
  * http/get/api/user/{user-key}
  * http/get/api/user/{user-key}/hasPermission/{permssion-key}
  * http/get/api/user/{user-key}/hasRole/{role-key}
* ui
  * permissions view
  * roles view
  * users view
