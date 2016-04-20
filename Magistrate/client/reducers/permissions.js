const permissions = (state = [], action) => {

  switch (action.type) {
    case "COLLECTIONS_CHANGED":
      return action.permissions;

    case "DELETE_PERMISSION":
      return state.filter(r => r.key != action.key);

    default:
      return state;
  }
}

export default permissions;
