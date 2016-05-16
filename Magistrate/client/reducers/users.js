const user = (state, action) => {

  switch (action.type) {

    case "CREATE_USER":
      return {
        key: action.key,
        name: action.name,
        includes: [],
        revokes: [],
        roles: [],
      }

    default:
      return state;
  }

}

const  users = (state = [], action) => {

  switch (action.type) {

    case "CREATE_USER":
      return state.concat([ user(null, action) ]);

    case "UPDATE_USERS":
      return action.users;

    case "DELETE_USER":
      return state.filter(u => u.key != action.key);

    default:
      return state;
  }

}

export default users;
