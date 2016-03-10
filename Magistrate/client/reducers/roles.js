const roles = (state = [], action) => {

  switch (action.type) {
    case "COLLECTIONS_CHANGED":
      return action.roles;

    default:
      return state;
  }
}

export default roles;
