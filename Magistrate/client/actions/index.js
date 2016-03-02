export const createUser = (id, username) => {
  return {
    meta: { remote: true },
    type: "CREATE_USER",
    id,
    username
  }
}
