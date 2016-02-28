import React from 'react'
import Grid from '../grid'
import RoleTile from './roleTile'
import RoleSelector from './RoleSelector'

var RoleGrid = React.createClass({

  render() {
    return (
      <Grid
        name="Roles"
        tile={RoleTile}
        selector={RoleSelector}
        {...this.props}
      />
    );
  }
})

export default RoleGrid
