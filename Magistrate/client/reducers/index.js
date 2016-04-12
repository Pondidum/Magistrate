import { combineReducers } from 'redux'

import ui from './ui'
import users from './users'
import roles from './roles'
import permissions from './permissions'

import userValidation from './userValidation'
import { routerReducer } from 'react-router-redux'

export default combineReducers({
  ui,
  users,
  roles,
  permissions,
  userValidation,
  routing: routerReducer
});
