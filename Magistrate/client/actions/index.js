export const createUser = (key, name) => {
  return {
    meta: { remote: true },
    type: "CREATE_USER",
    key,
    name
  }
}

export const setState = (state) => {
  return {
    type: "SET_STATE",
    state: state
  }
}
