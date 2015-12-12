var UserOverview = React.createClass({

  render() {

    return (
      <Overview
        url={this.props.url}
        tile={UserTile}
        create={CreateUserDialog}
        navigate={this.props.navigate}
      />
    );

  }
});
