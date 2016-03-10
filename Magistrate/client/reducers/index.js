import { combineReducers } from 'redux'
import users from './users'
import userValidation from './userValidation'

export default combineReducers({
  users,
  userValidation
});
