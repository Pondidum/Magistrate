import React from 'react'

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
