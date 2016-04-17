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
