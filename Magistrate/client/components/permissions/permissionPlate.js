import React, { Component} from 'react'
import { connect } from 'react-redux'
import { routeActions } from 'react-router-redux'

import Plate from '../Plate'
import DeletePermissionDialog from './DeletePermissionDialog'

const mapDispatchToProps = (dispatch) => {
  return {
    navigate: (key) => dispatch(routeActions.push(`/permissions/${key}`))
  }
}

class PermissionPlate extends Component {

  render() {
    const { content, navigate } = this.props;

    return (
      <Plate
        title={content.name}
        additions={ <DeletePermissionDialog permission={content} ref={d => this.dialog = d} />}
        onClick={e => { e.preventDefault(); navigate(content.key); }}
        onCross={e => { e.preventDefault(); this.dialog.getWrappedInstance().open(); }}>
        {content.description}
      </Plate>
    )
  }
}

export default connect(null, mapDispatchToProps)(PermissionPlate)
