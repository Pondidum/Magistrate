import React, { Component } from 'react'
import { connect } from 'react-redux'
import { routeActions } from 'react-router-redux'

import Plate from '../Plate'
import DeleteRoleDialog from './DeleteRoleDialog'

const mapDispatchToProps = (dispatch) => {
  return {
    navigate: (key) => dispatch(routeActions.push(`/roles/${key}`))
  }
}

class RolePlate extends Component {

  render() {
    const { content, navigate } = this.props;

    return (
      <Plate
        title={content.name}
        additions={<DeleteRoleDialog role={content} ref={d => this.dialog = d} />}
        onClick={e => { e.preventDefault(); navigate(content.key); }}
        onCross={() => this.dialog.getWrappedInstance().open()}>
        {content.description}
      </Plate>
    )
  }
}

export default connect(null, mapDispatchToProps)(RolePlate)
