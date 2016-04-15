import React, { Component } from 'react'
import { connect } from 'react-redux'
import { routeActions } from 'react-router-redux'

import Plate from '../plate'
import DeleteUserDialog from './DeleteUserDialog'

const mapDispatchToProps = (dispatch) => {
  return {
    navigate: (key) => dispatch(routeActions.push(`/users/${key}`))
  }
}

class UserPlate extends Component {

  render () {
    const { content, navigate } = this.props;
    return (
      <Plate
        title={content.name}
        additions={<DeleteUserDialog user={content} ref={d => this.dialog = d} />}
        onClick={e => { e.preventDefault(); navigate(content.key); }}
        onCross={() => this.dialog.getWrappedInstance().open()}>
        <span>Roles: {content.roles.length}</span>
        <span>Includes: {content.includes.length}</span>
        <span>Revokes: {content.revokes.length}</span>
      </Plate>
    )
  }
}

export default  connect(null, mapDispatchToProps)(UserPlate)
