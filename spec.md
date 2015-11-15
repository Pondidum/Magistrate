

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



# Specs

## User

* when a user has no Roles or Permissions
  * AddRole adds a role
  * RemoveRole does nothing
  * AddInclude adds to the includes collection
  * AddRevoke adds to the revokes collection
  * RemoveInclude does nothing
  * RemoveRevoke does nothing

* when a user has one included Permissions
  * AddInclude a different permission adds to the includes collection
  * AddInclude the same permission does nothing
  * RemoveInclude a different permission does nothing
  * RemoveInclude the same permission removes it from the includes collection
  * AddRevoke a different permission adds to the revokes collection
  * AddRevoke the same permission removes from the includes collection
  * RemoveRevoke a different permission does nothing
  * RemoveRevoke the same permission does nothing

* when a user has one revoked permission
  * AddInclude a different permission adds to the includes collection
  * AddInclude the same permission removes from the revokes collection and adds to the includes collection
  * RemoveInclude a different permission does nothing
  * RemoveInclude the same permission does nothing
  * AddRevoke a different permission adds to the revokes collection
  * AddRevoke the same permission does nothing
  * RemoveRevoke a different permission does nothing
  * RemoveRevoke the same permission removes from the revokes collection

* when a user has one role with a permission
  * AddInclude a different permission adds to the includes collection
  * AddInclude the same permission does nothing
  * RemoveInclude a different permission does nothing
  * RemoveInclude the same permission does nothing
  * AddRevoke a different permission adds to the revokes collection
  * AddRevoke the same permission adds to the revokes collection
  * RemoveRevoke a different permission does nothing
  * RemoveRevoke the same permission does nothing
