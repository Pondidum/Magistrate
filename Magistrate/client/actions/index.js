export const createUser = (key, name) => {
  return {
    meta: { remote: true },
    type: "CREATE_USER",
    key,
    name
  }
}

export const validateUser = (key) => {
  return {
    meta: { remote: true },
    type: "IS_USER_VALID",
    key
  }
}

export const setState = (state) => {
  return Object.assign({ type: "" }, state);
}

export const setTileSize = (size) => {
  return {
    type: "SET_TILE_SIZE",
    size: size
  }
}

export const deleteUser = (key) => {
  return {
    meta: { remote: true },
    type: "DELETE_USER",
    key
  }
}

export const renameUser = (key, newName) => {
  return {
    meta: { remote: true },
    type: "RENAME_USER",
    key,
    newName
  }
}

export const deleteRole = (key) => {
  return {
    meta: { remote: true },
    type: "DELETE_ROLE",
    key
  }
}

export const renameRole = (key, newName) => {
  return {
    meta: { remote: true},
    type: "RENAME_ROLE",
    key,
    newName
  }
}

export const deletePermission = (key) => {
  return {
    meta: { remote: true },
    type: "DELETE_PERMISSION",
    key
  }
}

export const updateUsers = () => {
  return {
    meta: {
      remote: true,
      url: "/api/users",
      success: (store, data) =>
        store.dispatch({ type: "UPDATE_USERS", users: data })
    },
    type: "UPDATE_USERS_REQUEST"
  }
}

export const updateRoles = () => {
  return {
    meta: {
      remote: true,
      url: "/api/roles",
      success: (store, data) =>
        store.dispatch({ type: "UPDATE_ROLES", roles: data })
    },
    type: "UPDATE_ROLES_REQUEST"
  }
}

export const updatePermissions = () => {
  return {
    meta: {
      remote: true,
      url: "/api/permissions",
      success: (store, data) =>
        store.dispatch({ type: "UPDATE_PERMISSIONS", permissions: data })
    },
    type: "UPDATE_PERMISSIONS_REQUEST"
  }
}

export const updateHistory = () => {
  return {
    meta: {
      remote: true,
      url: "/api/history",
      success: (store, data) =>
        store.dispatch({ type: "UPDATE_HISTORY", history: data })
    },
    type: "UPDATE_HISTORY_REQUEST"
  }
}
