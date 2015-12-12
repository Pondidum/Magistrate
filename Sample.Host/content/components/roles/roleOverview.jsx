var RoleOverview = React.createClass({

  render() {

    return (
      <Overview
        url={this.props.url}
        tile={RoleTile}
        create={CreateRoleDialog}
        navigate={this.props.navigate}
      />
    );

  }

});
