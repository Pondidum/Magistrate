import React, { Component } from 'react'
import { connect } from 'react-redux'
import { renameUser } from '../../actions'

import InlineEditor from '../InlineEditor'

const mapStateToProps = (state, ownProps) => {
  return {
    user: state.users.find(u => u.key == ownProps.params.key)
  }
}
const mapDispatchToProps = (dispatch) => {
  return {
    renameUser: (key, newName) => dispatch(renameUser(key, newName))
  }
}

const SingleUserView = ({ user, renameUser }) => (
  <div className="well">
    <h1>
      <InlineEditor
        initialValue={user.name}
        onChange={(newName) => renameUser(user.key, newName)}
      />
      <small className="pull-right">{user.key}</small>
    </h1>

    <div className="page-header">
      <a href="#">Change Roles...</a>
    </div>

    <div className="page-header">
      <a href="#">Change Includes...</a>
    </div>

    <div className="page-header">
      <a href="#">Change Revokes...</a>
    </div>

  </div>
)

export default connect(mapStateToProps, mapDispatchToProps)(SingleUserView)
