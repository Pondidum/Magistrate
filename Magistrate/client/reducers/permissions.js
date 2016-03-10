const permissions = (state = [], action) => {

  switch (action.type) {
    case "COLLECTIONS_CHANGED":
      return action.permissions;

    default:
      return state;
  }
}

export default permissions;
