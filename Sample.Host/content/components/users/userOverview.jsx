var UserOverview = React.createClass({

  render() {

    return (
      <Overview
        tile={UserTile}
        create={CreateUserDialog}
        collection={this.props.collection}
        {...this.props}
      />
    );

  }
});
