import React, { Component } from 'react'
import Plate from '../plate'
import DeleteUserDialog from './DeleteUserDialog'

class UserPlate extends Component {

  render () {
    const { content } = this.props;
    return (
      <Plate
        title={content.name}
        additions={<DeleteUserDialog user={content} ref={d => this.dialog = d} />}
        onCross={() => this.dialog.getWrappedInstance().open()}>
        <span>Roles: {content.roles.length}</span>
        <span>Includes: {content.includes.length}</span>
        <span>Revokes: {content.revokes.length}</span>
      </Plate>
    )
  }
}

export default UserPlate
