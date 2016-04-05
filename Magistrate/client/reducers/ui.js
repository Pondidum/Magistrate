
const ui = (state= {}, action) => {

  switch (action.type) {
    case "SET_TILE_SIZE":
      return Object.assign({}, state, { tileSize: action.size });

    default:
      return state;
  }
}

export default ui
