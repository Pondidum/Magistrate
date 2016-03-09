import { combineReducers } from 'redux'
import users from './users'

const userValidation = (state = {}, action) => {

  switch (action.type) {

    case "IS_USER_VALID_RESPONSE":
      return {
        isValid: action.isValid
      };

    default:
      return state;
  }
}

export default combineReducers({
  users,
  userValidation
});
