var UserView = React.createClass({

  render() {
    return (
      <div>
        <h1>{this.props.id}</h1>

        <h4>Roles</h4>
        <hr />
        <div className="row"></div>

        <h4>Permissions</h4>
        <hr />
        <div className="row"></div>


      </div>
    );
  }

});
