import React from 'react'
import Overview from '../overview'
import RoleTile from './roleTile'
import CreateRoleDialog from './CreateRoleDialog'

var RoleOverview = React.createClass({

  render() {

    return (
      <Overview
        tile={RoleTile}
        create={CreateRoleDialog}
        collection={this.props.collection}
        {...this.props}
      />
    );

  }

});

export default RoleOverview
