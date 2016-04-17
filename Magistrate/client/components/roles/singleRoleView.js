import React, { Component } from 'react'
import { connect } from 'react-redux'
import { renameRole } from '../../actions'

import InlineEditor from '../InlineEditor'

const mapStateToProps = (state, ownProps) => {
  return {
    role: state.roles.find(r => r.key == ownProps.params.key)
  }
}

const mapDispatchToProps = (dispatch) => {
  return {
    renameRole: (key, newName) => dispatch(renameRole(key, newName))
  }
}

const SingleRoleView = ({ role, renameRole }) => (
  <div className="well">
    <h1>
      <InlineEditor
        initialValue={role.name}
        onChange={(newName) => renameRole(role.key, newName)}
      />
      <small className="pull-right">{role.key}</small>
    </h1>

    <div className="page-header">
      <a href="#">Change Permissions</a>
    </div>

  </div>
)

export default connect(mapStateToProps, mapDispatchToProps)(SingleRoleView)
