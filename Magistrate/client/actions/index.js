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
  return state || { type: "" };
}
